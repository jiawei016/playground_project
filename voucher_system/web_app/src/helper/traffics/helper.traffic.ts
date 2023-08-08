import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";

@Injectable()
export class HelperTraffic {
    constructor(private http: HttpClient){

    }

    _PostApiCall(apiUrl: string, _data: any): Observable<any> {
        return this.http.post(apiUrl, _data).pipe(
            catchError((error) => {
                console.error('API Error:', error);
                return throwError(error);
            })
        );
    }
}