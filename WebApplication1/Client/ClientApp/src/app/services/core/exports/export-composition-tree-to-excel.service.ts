import {Injectable} from '@angular/core';
import * as XLSX from 'xlsx';
import {saveFile} from '../../../utils/file/file-bridge';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Injectable()
export class ExportCompositionTreeToExcelService {

  constructor() { }

  public static treeToJSON(tree, fileName) {
    const exporData = [];
    let rowData = {};
    let maxLevel;

    // Максимальное число уровней
    maxLevel = Math.max.apply(Math, tree.serializedValue.map( row => { return row.level }));

    // Резервирование ячеек
    for (let a = 0; a <= maxLevel; a++) {
      rowData['Уровень ' + (a+1)] = null;
    }

    // Обрабатываем каждую строку
    for (let i = 0; i < tree.serializedValue.length; i++) {
      let level = tree.serializedValue[i].level;

      rowData['Уровень ' + (level+1)] = (tree.serializedValue[i]['node']['data']['fullDesignation']);
      rowData['Инвентарный номер'] = (tree.serializedValue[i]['node']['data']['inventoryNumber']);
      rowData['ПИ'] = (tree.serializedValue[i]['node']['data']['actualPreAnnouncement']);
      rowData['Номер тома'] = (tree.serializedValue[i]['node']['data']['volumeNumber']);
      rowData['Доп. аттрибут'] = (tree.serializedValue[i]['node']['data']['elementSpecialAttribute']) ? (tree.serializedValue[i]['node']['data']['elementSpecialAttribute']['specialAttributeName']) : null;

      exporData.push(rowData);
      rowData = {};
    }
    ExportCompositionTreeToExcelService.exportAsExcelFile(exporData, fileName);
  }

  public static exportAsExcelFile(json: any[], excelFileName: string): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json,  {skipHeader: false});
    const workbook: XLSX.WorkBook = {
      Sheets: { 'Лист 1' : worksheet },
      SheetNames: ['Лист 1']
    };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    saveFile(excelBuffer, excelFileName + EXCEL_EXTENSION, EXCEL_TYPE);
  }
}
