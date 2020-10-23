import { Injectable, Inject } from '@angular/core';
import { Flights } from '../Interfaces';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class FlightService {
  baseUrl: string;

  constructor(protected http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public GetFlight(origin: string, destination: string, flightDate: string): Observable<Flights[]> {
    const params = new HttpParams()
      .set('origin', origin)
      .set('destination', destination)
      .set('from', flightDate);

    return this.http.get<Flights[]>(this.baseUrl + "api/flights", {params});
  }

  public SaveFlight(flight: Flights): Observable<Flights> {
    return this.http.post<Flights>(this.baseUrl + "api/flights", flight);
  }

  public GetFlightsFromDB() {
    return this.http.get<Flights[]>(this.baseUrl + "api/flights/db");
  }
}
