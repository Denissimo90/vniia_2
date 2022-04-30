package net.vniia.techdocs.reports;

import net.vniia.common.report.ExportType;
import net.vniia.techdocs.dto.DatasetDto;
import net.vniia.techdocs.dto.DatasetDto.PlaceStatistic;
import net.vniia.techdocs.services.StatisticService;
import org.apache.poi.ss.usermodel.BorderStyle;
import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.util.CellRangeAddress;
import org.apache.poi.xddf.usermodel.chart.*;
import org.apache.poi.xssf.usermodel.*;
import org.openxmlformats.schemas.drawingml.x2006.chart.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.List;
import java.util.Map;

@Service
public class StatisticReport {

    @Autowired
    private StatisticService statisticService;

    @Autowired
    RestTemplate restTemplate;

    public ResponseEntity<?> getDigitPaperRatio(int year, ExportType exportType) throws IOException, InterruptedException {
        String title = String.format("Соотношение электронной и бумажной КД за %d год", year);
        if (exportType.equals(ExportType.pdf))
            return buildResponseEntity(excel2Pdf(getPieExcel(statisticService.getDigitPaperRatio(year), title)), "report.pdf");
        return buildResponseEntity(getPieExcel(statisticService.getDigitPaperRatio(year), title), "report.xlsx");
    }

    public ResponseEntity<?> getSpecificationFile(int year, int quarter, ExportType exportType) throws IOException, InterruptedException {
        String title = String.format("Данные по спецификациям, выпущенным в %d году в %d квартале", year, quarter);
        List<DatasetDto> list = this.statisticService.getSpecificationData(year, quarter);
        if (exportType.equals(ExportType.pdf))
            return buildResponseEntity(excel2Pdf(getBarExcel(list, title)), "report.pdf");
        return buildResponseEntity(getBarExcel(list, title), "report.xlsx");
    }

    public ResponseEntity<?> getDetailFile(int year, int quarter, ExportType exportType) throws IOException, InterruptedException {
        String title = String.format("Данные по чертежам деталей, выпущенным в %d году в %d квартале", year, quarter);
        List<DatasetDto> list = this.statisticService.getDetailData(year, quarter);
        if (exportType.equals(ExportType.pdf))
            return buildResponseEntity(excel2Pdf(getBarExcel(list, title)), "report.pdf");
        return buildResponseEntity(getBarExcel(list, title), "report.xlsx");
    }

    public ResponseEntity<?> getDocumentFile(int year, int quarter, ExportType exportType) throws IOException, InterruptedException {
        String title = String.format("Данные по учтенной подразделение 33 КД в %d году в %d квартале", year, quarter);
        List<DatasetDto> list = this.statisticService.getPostedDocuments(year, quarter);
        if (exportType.equals(ExportType.pdf))
            return buildResponseEntity(excel2Pdf(getBarExcel(list, title)), "report.pdf");
        return buildResponseEntity(getBarExcel(list, title), "report.xlsx");
    }

    private byte[] getPieExcel(Map<String, Long> map, String title) throws IOException {
        XSSFWorkbook wb = new XSSFWorkbook();
        XSSFSheet sheet = wb.createSheet("Данные");
        final int NUM_OF_COLUMNS = map.size();
        Row row;
        Cell cell;

        row = sheet.createRow(0);
        Row secondRow = sheet.createRow(1);

        int i = 0;
        for (String key : map.keySet()) {
            cell = row.createCell(i);
            switch (key) {
                case "digit":
                    cell.setCellValue("На электронных носителях");
                    break;
                case "secret":
                    cell.setCellValue("Секретные документы");
                    break;
                case "paper":
                    cell.setCellValue("В бумажной форме");
                    break;
            }
            secondRow.createCell(i++).setCellValue(map.get(key));
        }

        XSSFDrawing drawing = sheet.createDrawingPatriarch();
        XSSFClientAnchor anchor = drawing.createAnchor(0, 0, 0, 0, 0, 4, 10, 25);

        XSSFChart chart = drawing.createChart(anchor);
        chart.setTitleText(title);
        chart.setTitleOverlay(false);
        XDDFChartLegend legend = chart.getOrAddLegend();
        legend.setPosition(LegendPosition.TOP);

        XDDFDataSource<String> cat = XDDFDataSourcesFactory.fromStringCellRange(sheet,
                new CellRangeAddress(0, 0, 0, NUM_OF_COLUMNS - 1));
        XDDFNumericalDataSource<Double> val = XDDFDataSourcesFactory.fromNumericCellRange(sheet,
                new CellRangeAddress(1, 1, 0, NUM_OF_COLUMNS - 1));

        XDDFChartData data =
                new XDDFPieChartData(chart.getCTChart().getPlotArea().addNewPieChart());

        data.setVaryColors(true);
        data.addSeries(cat, val);
        chart.plot(data);

        byte[][] colors = new byte[][]{
                new byte[]{(byte) 102, (byte) 181, (byte) 239},
                new byte[]{(byte) 103, (byte) 183, (byte) 94},
                new byte[]{(byte) 244, (byte) 100, (byte) 161}};

        for (int j = 0; j < 3; j++) {
            chart.getCTChart().getPlotArea().getPieChartArray(0).getSerArray(0).addNewDPt().addNewIdx().setVal(j);
            chart.getCTChart().getPlotArea().getPieChartArray(0).getSerArray(0).getDPtArray(j)
                    .addNewSpPr().addNewSolidFill().addNewSrgbClr().setVal(colors[j]);
        }

        CTDLbls lbls = chart.getCTChart().getPlotArea().getPieChartArray(0).getSerArray(0).addNewDLbls();

        lbls.addNewShowLegendKey().setVal(false);
        lbls.addNewShowPercent().setVal(false);
        lbls.addNewShowSerName().setVal(false);
        lbls.addNewShowCatName().setVal(false);

        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        wb.write(bos);
        return bos.toByteArray();
    }

    private byte[] getBarExcel(List<DatasetDto> list, String title) throws IOException, InterruptedException {
        XSSFWorkbook wb = new XSSFWorkbook();

        XSSFSheet sheet = wb.createSheet("Данные");


        setCat(sheet.createRow(0));
        setDataBarExcel(list, sheet);

        XSSFSheet chartSheet = wb.createSheet("График");
        XSSFDrawing drawing = chartSheet.createDrawingPatriarch();
        XSSFClientAnchor anchor = drawing.createAnchor(0, 0, 0, 0, 0, 0, 14, 25);

        XSSFChart chart = drawing.createChart(anchor);
        chart.setTitleText(title);
        chart.setTitleOverlay(false);

        if (chart.getCTChart().getAutoTitleDeleted() == null)
            chart.getCTChart().addNewAutoTitleDeleted();
        chart.getCTChart().getAutoTitleDeleted().setVal(false);

        XDDFChartLegend legend = chart.getOrAddLegend();
        legend.setPosition(LegendPosition.TOP);

        CTChart ctChart = chart.getCTChart();
        CTPlotArea ctPlotArea = ctChart.getPlotArea();

        CTBarChart ctBarChart = ctPlotArea.addNewBarChart();
        CTBoolean ctBoolean = ctBarChart.addNewVaryColors();
        ctBoolean.setVal(true);
        ctBarChart.addNewBarDir().setVal(STBarDir.COL);
        ctBarChart.addNewGrouping().setVal(STBarGrouping.STACKED);
        ctBarChart.addNewOverlap().setVal((byte) 100);


        ctBarChart.addNewAxId().setVal(123456);
        ctBarChart.addNewAxId().setVal(123457);

        byte[] axisColor = new byte[]{(byte) 236, (byte) 235, (byte) 234};

        CTCatAx ctCatAx = ctPlotArea.addNewCatAx();
        ctCatAx.addNewAxId().setVal(123456);
        CTScaling ctScaling = ctCatAx.addNewScaling();
        ctScaling.addNewOrientation().setVal(STOrientation.MIN_MAX);
        ctCatAx.addNewDelete().setVal(false);
        ctCatAx.addNewAxPos().setVal(STAxPos.B);
        ctCatAx.addNewCrossAx().setVal(123457);
        ctCatAx.addNewTickLblPos().setVal(STTickLblPos.NEXT_TO);
        ctCatAx.addNewSpPr().addNewLn().addNewSolidFill()
                .addNewSrgbClr().setVal(axisColor);
        ctCatAx.addNewNoMultiLvlLbl().setVal(false);


        CTValAx ctValAx = ctPlotArea.addNewValAx();
        ctValAx.addNewAxId().setVal(123457);
        ctScaling = ctValAx.addNewScaling();
        ctScaling.addNewOrientation().setVal(STOrientation.MIN_MAX);
        ctValAx.addNewDelete().setVal(false);
        ctValAx.addNewAxPos().setVal(STAxPos.L);
        ctValAx.addNewCrossAx().setVal(123456);
        ctValAx.addNewTickLblPos().setVal(STTickLblPos.NEXT_TO);
        ctValAx.addNewSpPr().addNewLn().addNewSolidFill()
                .addNewSrgbClr().setVal(axisColor);
        ctValAx.addNewMajorGridlines();

        byte[][] seriesColors = new byte[][]{
                new byte[]{(byte) 102, (byte) 181, (byte) 239},
                new byte[]{(byte) 244, (byte) 100, (byte) 161}
        };

        for (int i = 0; i < 2; i++) {
            CTBarSer ctBarSer = ctBarChart.addNewSer();
            CTSerTx ctSerTx = ctBarSer.addNewTx();
            CTStrRef ctStrRef = ctSerTx.addNewStrRef();
            ctStrRef.setF(
                    new CellRangeAddress(0, 0, i + 2, i + 2)
                            .formatAsString(sheet.getSheetName(), true));
            ctBarSer.addNewIdx().setVal(i);

            CTAxDataSource cttAxDataSource = ctBarSer.addNewCat();
            CTMultiLvlStrRef ctMultiLvlStrRef = cttAxDataSource.addNewMultiLvlStrRef();
            ctMultiLvlStrRef.setF(
                    new CellRangeAddress(1, sheet.getLastRowNum(), 0, 1)
                            .formatAsString(sheet.getSheetName(), true));

            CTNumDataSource ctNumDataSource = ctBarSer.addNewVal();
            CTNumRef ctNumRef = ctNumDataSource.addNewNumRef();
            ctNumRef.setF(
                    new CellRangeAddress(1, sheet.getLastRowNum(), i + 2, i + 2)
                            .formatAsString(sheet.getSheetName(), true));

            ctBarSer.addNewSpPr().addNewSolidFill().addNewSrgbClr().setVal(seriesColors[i]);
        }
        chartSheet.setFitToPage(true);
        chartSheet.getPrintSetup().setLandscape(true);
        autoSizeColumn(sheet);
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        wb.write(bos);
        return bos.toByteArray();
    }

    private ResponseEntity<?> buildResponseEntity(byte[] data, String filename) {
        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + filename + "\"")
                .body(data);
    }

    private void setDataBarExcel(List<DatasetDto> list, XSSFSheet sheet) {
        XSSFCellStyle style = sheet.getWorkbook().createCellStyle();
        style.setBorderBottom(BorderStyle.THIN);
        style.setBorderTop(BorderStyle.THIN);
        style.setBorderRight(BorderStyle.THIN);
        style.setBorderLeft(BorderStyle.THIN);

        Row row;
        Cell cell;
        int rowIndex = 1;
        for (int i = 0; i < list.size(); i++) {
            for (PlaceStatistic stat : list.get(i).getPlaceStatistics()) {
                if (i == 0)
                    sheet.createRow(rowIndex).createCell(0).setCellValue(stat.getPlace());
                Map<String, Long> map = stat.getDepartmentStatistic();
                for (String key : map.keySet()) {
                    row = sheet.getRow(rowIndex);
                    if (row == null)
                        row = sheet.createRow(rowIndex);
                    if (i == 0) {
                        cell = row.createCell(1);
                        cell.setCellValue(key);
//                        cell.setCellStyle(style);
                    }
                    cell = row.createCell(i + 2);
                    cell.setCellValue(map.get(key));
                    rowIndex++;
//                    cell.setCellStyle(style);
                }
            }
            rowIndex = 1;
        }
    }

    private void autoSizeColumn(XSSFSheet sheet) {
        for (int i = 0; i < 4; i++)
            sheet.autoSizeColumn(i);
    }

    public byte[] excel2Pdf(byte[] data) {
//        HttpHeaders headers = new HttpHeaders();
//        headers.setContentType(MediaType.MULTIPART_FORM_DATA);
//        MultiValueMap<String, Object> parts = new LinkedMultiValueMap<>();
//        ByteArrayResource res = new ByteArrayResource(data) {
//            @Override
//            public String getFilename() {
//                return "report.xlsx";
//            }
//        };
//        parts.add("file", res);
//        long time = System.currentTimeMillis();
//        ResponseEntity<byte[]> entity = restTemplate.exchange("http://35-docker-3:3000/convert/office",
//                HttpMethod.POST,
//                new HttpEntity<>(parts, headers), byte[].class);
//        System.out.println(time - System.currentTimeMillis());
//        return entity.getBody();
        return null;
    }

    private void setCat(Row row) {
        row.createCell(0).setCellValue("Площадка");
        row.createCell(1).setCellValue("Подразделение");
        row.createCell(2).setCellValue("В бумажной форме");
        row.createCell(3).setCellValue("На электронных носителях");
    }

}





