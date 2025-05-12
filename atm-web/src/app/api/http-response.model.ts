export interface ApiResponse<T> {
    errors?: any;
    data?: T;
    message?: any;
    isSuccessful?: boolean;
    errorField?: string;
}
