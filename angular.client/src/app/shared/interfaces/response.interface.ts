export interface HttpResponse<T> {
    data: T
    succeded: boolean
    message: string
}