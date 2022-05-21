import {Component, OnInit} from '@angular/core';
import {ReportService} from '../../../../service/report.service';
import {CalendarModule} from 'primeng/calendar';
import {ReportEntity} from '../../../../dto/ReportEntity';

@Component({
  selector: 'app-main-report',
  templateUrl: './main-report.component.html',
  styleUrls: ['./main-report.component.css'],
  providers: [ReportService]
})
export class MainReportComponent implements OnInit {

  startYear: Date;
  endYear: Date;
  startDate: Date;
  endDate: Date;
  update: boolean;

  yearReportEntities: ReportEntity[] = [];
  selectedYearReportEntity: ReportEntity;
  pieOptions = {legend: {position: 'right'}};
  barOptions = {legend: {display: false}};
  generalOptions;

  yearChartData;

  constructor(private reportService: ReportService) {
  }

  ngOnInit(): void {
    this.startYear = new Date();
    this.endYear = new Date();

    this.yearReportEntities.forEach(p => {
      this.yearChartData.labels.push(p.period);
      this.yearChartData.datasets[0].data.push(this.yearReportEntities);
      this.yearChartData.datasets[0].backgroundColor.push(getRandomColor());
      this.yearChartData.datasets[0].borderColor.push(getRandomColor());
    });

    console.log(this.yearReportEntities);
    console.log(this.yearChartData);
  }

  onStartYearChange() {
    if (!this.update) {
      this.update = true;
      this.endYear.setDate(this.startYear.getDate() + 365);
      this.update = false;
    }
  }
}

function getRandomColor(): string {
  return 'rgb(' + Math.floor(Math.random() * 255) + ',' + Math.floor(Math.random() * 255) + ','
    + Math.floor(Math.random() * 255) + ')';
}
