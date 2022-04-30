import {Component, OnInit, ViewChild} from '@angular/core';
import {DocumentsService} from '../../services/document.service';
import {DialogService, FileViewerComponent} from '@prism/common';
import {StatisticPickerComponent} from '../forms/statistic-picker/statistic-picker.component';
import * as ChartPlugin from 'chartjs-plugin-hierarchical';
import * as LabelPlugin from 'chartjs-plugin-labels';
import {UIChart} from 'primeng';
import {jsPDF} from 'jspdf';


@Component({
  selector: 'app-document-statistics',
  templateUrl: './document-statistics.component.html',
  styleUrls: ['./document-statistics.component.css'],
  providers: [DocumentsService]
})
export class DocumentStatisticsComponent implements OnInit {

  data: any;
  plugins = [ChartPlugin, LabelPlugin];


  @ViewChild('dialogChart') dialogChart: UIChart;
  @ViewChild('specChart') specChart: UIChart;
  @ViewChild('pieChart') pieChart: UIChart;
  @ViewChild('detChart') detChart: UIChart;
  @ViewChild('docChart') docChart: UIChart;

  dates = {};
  specificationData: any;
  detailData: any;
  options: any;
  documentData: any;
  visible = false;
  dialogChartKey: string;
  loading = false;

  barOptions = {
    responsive: true,
    layout: {
      padding: {
        bottom: 20,
      }
    },
    scales: {
      xAxes: [
        {
          stacked: true,
          type: 'hierarchical',
          offset: true,
          gridlines: {
            offsetGridLines: true
          }
        }
      ],
      yAxes: [
        {
          scaleLabel: {
            labelString: 'Количество документов',
            display: true
          },
          stacked: true,
          ticks: {
            beginAtZero: true
          }
        }
      ]
    },
    plugins: {
      labels: false
    },
  };

  pieOptions = {
    plugins: {
      labels: {
        render: (data) => data.value + ` (${data.percentage}%)`,
        fontColor: '#000000',
        position: 'outside',
        textMargin: 6
      }
    }
  };
  
  constructor(public documentsService: DocumentsService,
              private dialogService: DialogService) {
  }

  async ngOnInit() {
    this.loading = true;
    await this.loadData();
    this.loading = false;
  }

  async loadData(year = new Date().getFullYear() - 1, quarter = Math.ceil(new Date().getMonth() / 3)) {
    this.setDates(year, quarter);
    for (const k of Object.keys(this.dates)) {
      if (k == 'pie') {
        await this.constructPie(year);
      } else {
        await this.constructBar(k, year, quarter);
      }
    }
  }

  setDates(year: number, quarter: number) {
    this.dates = {
      pie: {
        year: year
      },
      specification: {
        year: year,
        quarter: quarter
      },
      detail: {
        year: year,
        quarter: quarter
      },
      document: {
        year: year,
        quarter: quarter
      }
    };
  }

  async statPicker(key) {
    const dialog = this.dialogService.createDialog(StatisticPickerComponent);
    dialog.init(key, this.dates[key].year, this.dates[key].quarter);
    dialog.result.subscribe(async out => {
      this.loading = true;
      this.dates[key].year = out.year;
      if (key != 'pie') {
        this.dates[key].quarter = out.quarter;
        await this.constructBar(key, out.year, out.quarter);
      } else {
        await this.constructPie(out.year);
      }
      this.loading = false;
    });
  }

  async constructPie(year) {
    const map = await this.documentsService.getDigitPaperRation(year);
    this.data = {
      labels: [...Object.keys(map).map(key => {
        switch (key) {
          case 'paper':
            return 'В бумажной форме';
          case 'digit':
            return 'В электронной форме';
          case 'secret':
            return 'Секретные документы';
        }
      })],
      datasets: [{
        data: [...Object.keys(map).map(key => map[key])],
        backgroundColor: [
          '#36A2EB',
          '#569750',
          '#ff6384'
        ]
      }]
    };
  }

  async constructBar(key, year, quarter) {
    let datasets = [];
    let labels = [];
    const datasetList = await this.documentsService.getBarData(key, year, quarter);
    datasetList.forEach(e => {
      datasets.push({
        backgroundColor: e.label == 'digit' ? '#ff6384' : '#36A2EB',
        label: e.label == 'digit' ? 'В электронном формате (Windchill)' : 'В бумажной форме',
        tree: e.placeStatistics.map(el => ({
          value: el.totalValue,
          children: Object.keys(el.departmentStatistic).map(key => el.departmentStatistic[key])
        }))
      });
    });

    datasetList[0].placeStatistics.forEach(el => {
      labels.push({
        label: el.place,
        expand: false,
        children: Object.keys(el.departmentStatistic)
      });
    });

    const data = {labels: labels, datasets: datasets};
    switch (key) {
      case 'specification':
        this.specificationData = data;
        break;
      case 'detail':
        this.detailData = data;
        break;
      case 'document':
        this.documentData = data;
    }
  }

  getHeader(key) {
    const year = this.dates[key]?.year;
    const quarter = key !== 'pie' ? this.dates[key]?.quarter : null;
    switch (key) {
      case 'pie':
        return 'Соотношение электронной и бумажной КД за ' + year + ' год';
      case 'specification':
        return 'Данные по спецификациям, выпущенным в ' + year + ' году в ' + quarter + ' квартале';
      case 'detail':
        return 'Данные по чертежам деталей, выпущенным в ' + year + ' году в ' + quarter + ' квартале';
      case 'document':
        return 'Данные по учтенной подразделение 33 КД в ' + year + ' году в ' + quarter + ' квартале';
    }
  }

  showDialog(key: string) {
    this.dialogChartKey = key;
    switch (key) {
      case 'detail':
        this.dialogChart.data = this.detailData;
        break;
      case 'specification':
        this.dialogChart.data = this.specificationData;
        break;
      case 'document':
        this.dialogChart.data = this.documentData;
        break;
    }
    this.visible = true;
  }

  async getFile(key: string, type: string) {
    if (key === 'pie' && type === 'xlsx') {
      await this.documentsService.getPieFile(this.dates[key].year, type);
    } else {
      if (type == 'xlsx') {
        await this.documentsService.getBarFile(key, this.dates[key].year, this.dates[key].quarter, type);
      } else {
        const doc = this.generatePdf(key);

        const dialog = this.dialogService.createDialog(FileViewerComponent);
        dialog.init(doc.output('blob'), 'application/pdf', 'График.pdf');
      }
    }
  }

  reload(key: string) {
    this.loading = true;
    if (key == 'pie') {
      this.constructPie(this.dates[key].year);
    } else {
      this.constructBar(key, this.dates[key].year, this.dates[key].quarter);
    }
    this.loading = false;
  }

  generatePdf(key) {
    const doc = new jsPDF('l', 'px', 'a4');
    const size = doc.internal.pageSize;
    const imgProps = doc.getImageProperties(this.getChart(key).getBase64Image());
    const x = (size.getWidth() - imgProps.width * 0.55) / 2;
    const y = (size.getHeight() - imgProps.height * 0.55) / 2;
    const title = this.getHeader(key);
    doc.addFont('assets/fonts/Roboto-Light.ttf', 'Roboto-Light', 'normal');
    doc.setFont('Roboto-Light');
    doc.addImage(this.getChart(key).getBase64Image(), 'png', x, y, imgProps.width * 0.55, imgProps.height * 0.55);
    doc.text(title, (size.getWidth() - title.length * 6) / 2, 20);
    return doc;
  }

  getChart(key: string) {
    switch (key) {
      case 'pie':
        return this.pieChart;
      case 'specification':
        return this.specChart;
      case 'detail':
        return this.detChart;
      case 'document':
        return this.docChart;
    }
  }
}

