export interface HttpResponse<T> {
    data: T
    succeeded: boolean
    message: string
}
