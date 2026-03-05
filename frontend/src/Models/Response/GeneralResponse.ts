export class GeneralResponse<T>{
    constructor(
        public StatusCode: number,
        public Message: string,
        public Object: T
    ) {}
}