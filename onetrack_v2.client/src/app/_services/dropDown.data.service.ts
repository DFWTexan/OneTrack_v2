import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class DropdownDataService {
    private scoreNumbers: string[] = [];
    private employerAgencies: string[] = [];
    private licenseStatuses: string[] = [];
    private licenseNames: string[] = [];

    constructor(private http: HttpClient) {}

    getScoreNumbers() {
        // return this.http.get<string[]>('/api/agentstatuses');
        return this.scoreNumbers;
    }

    getEmployerAgencies() {
        // return this.http.get<string[]>('/api/states');
        return this.employerAgencies;
    }

    getLicenseStatuses() {
        // return this.http.get<string[]>('/api/stateprovinces');
        return this.licenseStatuses;
    }

    getLicenseNames() {
        // return this.http.get<string[]>('/api/stateprovinces');
        return this.licenseNames;
    }
}