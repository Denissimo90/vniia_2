import {Component, OnInit} from '@angular/core';
import {ResultService} from '../../dashboard/chillout-zone/core/result.service';
import {LazyLoadEvent, PageQuery} from '@prism/common';
import {FinancialAidOwnerTypeEnum} from '../../../domain/gen/financialaidownertypeenum';
import {FinancialAidClaimService} from '../../../services/gen/financialaidclaim.service';
import {FinancialAidClaimStatusLocaleEnum} from '../../../domain/enums/FinancialAidClaimStatusLocaleEnum';
import {FinancialAidClaimStatusEnum} from '../../../domain/gen/financialaidclaimstatusenum';
import {FinancialAidClaimDto} from '../../../domain/gen/financialaidclaimdto';
import {FinancialAidSourceLocaleEnum} from '../../../domain/enums/FinancialAidSourceLocaleEnum';
import {FinancialAidSourceEnum} from '../../../domain/gen/financialaidsourceenum';

@Component({
  selector: 'app-financial-aid-statistics',
  templateUrl: './financial-aid-statistics.component.html',
  styleUrls: ['./financial-aid-statistics.component.css'],
  providers: [ResultService, FinancialAidClaimService]
})
export class FinancialAidStatisticsComponent implements OnInit {
  statusLocaleEnum = FinancialAidClaimStatusLocaleEnum;
  sourceLocaleEnum = FinancialAidSourceLocaleEnum;
  filter = false;
  chartData;
  pieOptions = {legend: {position: 'right'}};
  barOptions = {legend: {display: false}};
  generalOptions;
  claims: FinancialAidClaimDto[];
  rangeDates: Date[];
  tabIndex = 0;
  chartType;
  chartTypes = [
    {label: 'Бары', value: 'bar'},
    {label: 'Пирог', value: 'pie'},
    {label: 'Пончик', value: 'doughnut'},
    {label: 'Полярная звезда', value: 'polarArea'},
    {label: 'Радар', value: 'radar'},
    {label: 'Линейный', value: 'line'}
  ];

  constructor(
    private financialAidClaimService: FinancialAidClaimService
  ) {
  }

  async ngOnInit() {
    this.chartType = 'bar';
    this.generalOptions = this.barOptions;

    const startDate = new Date();
    const endDate = new Date();
    startDate.setDate(endDate.getDate() - 30);
    this.rangeDates = [startDate, endDate];
  }

  async onLoad(event: LazyLoadEvent) {
    try {
      const query = `registrationDate >= '${this.rangeDates[0].toLocaleDateString()}'
        and registrationDate < '${this.rangeDates[1].toLocaleDateString()}' and status != 'DRAFT'`;
      const pageQuery = new PageQuery({
        first: 0,
        rows: 30000,
        query: query,
        filters: event.pageQuery.filters,
        sorts: event.pageQuery.sorts,
        distinctColumn: event.pageQuery.distinctColumn,
        exportColumns: event.pageQuery.exportColumns
      });
      const list = await this.financialAidClaimService.searchSummaryClaims(FinancialAidOwnerTypeEnum.ALL, pageQuery);
      event.successCallback(list.items, list.total);

      if (!event.pageQuery.distinctColumn && !event.pageQuery.exportColumns) {
        this.claims = list.items;
        this.refreshCharts();
      }
    } catch (e) {
      event.failCallback();
      throw e;
    }
  }

  refreshCharts() {
    if (this.chartType === 'pie' || this.chartType === 'doughnut' || this.chartType === 'polarArea') {
      this.generalOptions = this.pieOptions;
    } else {
      this.generalOptions = this.barOptions;
    }
    setTimeout(() => this.reloadDataset());
  }

  reloadDataset() {
    switch (this.tabIndex) {
      case 0: {
        this.chartData = {
          labels: ['Выполненные', 'Отклонённые', 'Стаж "ВНИИА" < 20 лет', 'Стаж "ВНИИА" > 20 лет',
            'Стаж в отрасли < 25 лет', 'Стаж в отрасли > 25 лет', 'Для членов Профсоюза', 'Для работников "ВНИИА"'],
          datasets: [
            {
              label: 'Отчет по заявкам',
              backgroundColor: [getRandomColor(), getRandomColor(), getRandomColor(), getRandomColor(),
                getRandomColor(), getRandomColor(), getRandomColor(), getRandomColor()],
              data: [
                this.claims.length,
                this.claims.filter(p => p.status === FinancialAidClaimStatusEnum.REJECTED ||
                  p.status === FinancialAidClaimStatusEnum.RETURNED).length,
                this.claims.filter(p => p.enterpriseStageYear < 20).length,
                this.claims.filter(p => p.enterpriseStageYear >= 20).length,
                this.claims.filter(p => p.industryStageYear < 25).length,
                this.claims.filter(p => p.industryStageYear >= 25).length,
                this.claims.filter(p => p.source === FinancialAidSourceEnum.LABOR_UNION).length,
                this.claims.filter(p => p.source === FinancialAidSourceEnum.ENTERPRISE).length
              ]
            }
          ]
        };
        break;
      }
      case 1: {
        this.resetDataset('Статусы заявок');
        const states = this.claims.map(p => p.status);
        const distinctStates = states.filter((n, i) => states.indexOf(n) === i);

        distinctStates.forEach(p => {
          this.chartData.labels.push(FinancialAidClaimStatusLocaleEnum[p]);
          this.chartData.datasets[0].data.push(this.claims.filter(f => f.status === p).length);
          this.chartData.datasets[0].backgroundColor.push(this.onGetColumnStyleByStatus(p)['background-color']);
          this.chartData.datasets[0].borderColor.push(this.onGetColumnStyleByStatus(p)['background-color']);
        });
        break;
      }
      case 2: {
        this.resetDataset('Отчет по подразделениям');
        const departments = this.claims.map(p => p.applicant.department.code);
        const distinctDepartments = departments.filter((n, i) => departments.indexOf(n) === i);

        distinctDepartments.forEach(p => {
          this.chartData.labels.push('Подразделение ' + p);
          this.chartData.datasets[0].data.push(this.claims
            .filter(f => f.applicant.department.code === p).length);
          this.chartData.datasets[0].backgroundColor.push(getRandomColor());
          this.chartData.datasets[0].borderColor.push(getRandomColor());
        });
        break;
      }
      case 3: {
        this.resetDataset('Отчет по категориям');
        const categories = this.claims.map(p => p.category == null ? null : p.category.name);
        const distinctCategories = categories.filter((n, i) => categories.indexOf(n) === i);

        distinctCategories.forEach(p => {
          this.chartData.labels.push(p == null ? 'Без категории' : p);
          this.chartData.datasets[0].data.push(this.claims.filter(f => (f.category == null && p == null) ||
            (f.category != null && p != null && f.category.name === p)).length);
          this.chartData.datasets[0].backgroundColor.push(getRandomColor());
          this.chartData.datasets[0].borderColor.push(getRandomColor());
        });
        break;
      }
      case 4: {
        this.resetDataset('Отчёт по программам');
        const sources = this.claims.map(p => p.source);
        const distinctSources = sources.filter((s, i) => sources.indexOf(s) === i);
        distinctSources.forEach(p => {
          this.chartData.labels.push(p == null ? 'Программа не указана' : this.sourceLocaleEnum[p]);
          this.chartData.datasets[0].data.push(this.claims.filter(f => f.source === p).length);
          this.chartData.datasets[0].backgroundColor.push(this.getChartColorBySource(p)['background-color']);
          this.chartData.datasets[0].borderColor.push(this.getChartColorBySource(p)['background-color']);
        });
      }
    }
  }

  resetDataset(label: string) {
    this.chartData = {
      labels: [],
      datasets: [
        {
          label: label,
          backgroundColor: [],
          borderColor: [],
          data: []
        }
      ]
    };
  }

  getStatisticValue(value: string): any {
    if (this.claims != null) {
      switch (value) {
        case 'all':
          return this.claims.length;
        case 'rejected':
          return this.claims.filter(p => p.status === FinancialAidClaimStatusEnum.REJECTED ||
            p.status === FinancialAidClaimStatusEnum.RETURNED).length;
        case 'vniia20':
          return this.claims.filter(p => p.enterpriseStageYear < 20).length;
        case 'vniia20+':
          return this.claims.filter(p => p.enterpriseStageYear >= 20).length;
        case 'industry25':
          return this.claims.filter(p => p.industryStageYear < 25).length;
        case 'industry25+':
          return this.claims.filter(p => p.industryStageYear >= 25).length;
        case 'sum':
          let sum = 0;
          if (this.claims.filter(p => p.status === FinancialAidClaimStatusEnum.DONE).length === 0) {
            return 0;
          }
          this.claims.filter(p => p.status === FinancialAidClaimStatusEnum.DONE).forEach(s => sum += s.sum);
          return (sum / this.claims
            .filter(p => p.status === FinancialAidClaimStatusEnum.DONE).length).toFixed(4) + ' руб.';
        case 'source_enterprise':
          return this.claims.filter(p => p.source === FinancialAidSourceEnum.ENTERPRISE).length;
        case 'source_labor_union':
          return this.claims.filter(p => p.source === FinancialAidSourceEnum.LABOR_UNION).length;
      }
    }
    return '';
  }

  onGetColumnStyle(e) {
    if (e.data && e.data.status) {
      e.style = this.onGetColumnStyleByStatus(e.data.status);
    }
  }

  onGetColumnStyleByStatus(status: string): any {
    switch (status) {
      case FinancialAidClaimStatusEnum.IN_AGREEMENT_WITH_LABOR_ORGANIZER:
      case FinancialAidClaimStatusEnum.IN_AGREEMENT_WITH_EXECUTOR: {
        return {'background-color': 'rgb(255,240,220,1)', 'color': 'black'};
      }
      case FinancialAidClaimStatusEnum.IN_PROGRESS: {
        return {'background-color': 'rgb(230,240,255,1)', 'color': 'black'};
      }
      case FinancialAidClaimStatusEnum.DONE: {
        return {'background-color': 'rgb(200,240,210,1)', 'color': 'black'};
      }
      case FinancialAidClaimStatusEnum.REJECTED: {
        return {'background-color': 'rgb(255,199,199,1)', 'color': 'black'};
      }
      case FinancialAidClaimStatusEnum.RETURNED: {
        return {'background-color': 'rgb(255,249,184)', 'color': 'black'};
      }
    }
    return {'background-color': 'white', 'color': 'black'};
  }

  getChartColorBySource(source: string): any {
    switch (source) {
      case FinancialAidSourceEnum.LABOR_UNION: {
        return {'background-color': 'rgb(255,199,199,1)', 'color': 'black'}; //#b8d3fc
      }
      case FinancialAidSourceEnum.ENTERPRISE: {
        return {'background-color': 'rgb(184,211,252,1)', 'color': 'black'};
      }
      default: {
        return {'background-color': 'rgb(219,222,222)', 'color': 'black'};
      }
    }
  }
}

function getRandomColor(): string {
  return 'rgb(' + Math.floor(Math.random() * 255) + ',' + Math.floor(Math.random() * 255) + ','
    + Math.floor(Math.random() * 255) + ')';
}
