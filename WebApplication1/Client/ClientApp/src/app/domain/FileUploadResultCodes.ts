export enum FileUploadResultCodes { UPLOADED = 'UPLOADED',
                                    UNKNOWN_FILE_EXTENSION = 'UNKNOWN_FILE_EXTENSION',
                                    FILE_ALREADY_EXIST = 'FILE_ALREADY_EXIST',
                                    DOCUMENT_NOT_FOUND = 'DOCUMENT_NOT_FOUND',
                                    DOCUMENT_HAS_FILE = 'DOCUMENT_HAS_FILE'}

export class FileUploadResult {
    fileName: String;
    result: FileUploadResultCodes;
    message: String;

    constructor() {
    }
}
