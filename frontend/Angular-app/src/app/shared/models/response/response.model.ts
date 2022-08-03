export interface ResponseModel<T> {
    success: boolean,
    messages: string[],
    fieldMessages: FieldMessage[]
    error: string,
    data: T
}

interface FieldMessage {
    fieldName: string,
    messages: string[],
}