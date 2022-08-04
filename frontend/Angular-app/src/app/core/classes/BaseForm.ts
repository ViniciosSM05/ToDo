import { ResponseModel } from "src/app/shared/models/response/response.model";

export abstract class BaseForm<T> {
    _response!: ResponseModel<T>;
    constructor() { }

    get response() {
        return this._response;
    }
    set setResponse(resp: ResponseModel<T>) {
        this._response = resp;
    } 
}