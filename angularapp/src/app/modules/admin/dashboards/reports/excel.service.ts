import { Injectable } from "@angular/core";
import { Workbook } from "exceljs"
import fs from "file-saver"
@Injectable({
    providedIn: "root"
})
export class ExcelService {
    private _workbook: Workbook
    constructor() { }
    async downloadReport() {
        this._workbook = new Workbook()
        this._workbook.creator = 'Evaluator'

        const data = await this._workbook.xlsx.writeBuffer()
        const blob = new Blob([data])
        fs.saveAs(blob, "report.xlsx")
    }
}