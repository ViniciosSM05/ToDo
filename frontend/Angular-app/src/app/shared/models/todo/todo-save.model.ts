export interface TodoSaveModel {
    id: string | null,
    code: string,
    description: string,
    date: Date,
    userId: string
}