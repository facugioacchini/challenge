import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { Activities } from '../models/activities';

@Injectable()
export class ActivitiesService {
    constructor(private http: HttpClient) { }

    url = environment.url;

    public getActivities() {
        return this.http.get<Activities>(`${this.url}Activities/GetActivities`);
    }
}