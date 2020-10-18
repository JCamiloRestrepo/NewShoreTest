import { Injectable, Inject } from '@angular/core';
import { Flights } from '../Interfaces';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class FlightService {
  public algo: string = "Holaaa";
  baseUrl: string;

  constructor(protected http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public GetFlight(): Observable<Flights[]> {
    
    return this.http.get<Flights[]>(this.baseUrl + "api/flights");
  }
}
