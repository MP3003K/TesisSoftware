import { Injectable } from "@angular/core";
import fs from "file-saver"
class Cell {
    value
    alignment
}
class Sheet {
    getCell(str: string) { return new Cell() }
    mergeCells(str: string) { }
}
class Workbook {
    creator
    xlsx
    addWorksheet(str: string): Sheet { return new Sheet() }

}

@Injectable({
    providedIn: "root"
})
export class ExcelService {
    private _workbook: Workbook;
    constructor() { }
    async downloadReport(excel_data) {
        this._workbook = new Workbook()
        this._workbook.creator = 'Evaluator'
        this._createTable(excel_data)
        this._createDetail(excel_data)
        const data = await this._workbook.xlsx.writeBuffer()
        const blob = new Blob([data])
        fs.saveAs(blob, "report.xlsx")
    }

    async _createTable(data) {
        const sheet = this._workbook.addWorksheet("Reporte")

        sheet.mergeCells("B2:F2")
        const hse_title = sheet.getCell("B2")
        hse_title.value = 'Escala HSE'
        hse_title.alignment = { horizontal: 'center', vertical: 'middle' };

        sheet.mergeCells("H2:L2")
        const fr_title = sheet.getCell("H2")
        fr_title.value = 'Escala FR'
        fr_title.alignment = { horizontal: 'center', vertical: 'middle' };


    }

    async _createDetail(data) {

    }
}