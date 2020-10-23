import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FlightService } from '../service/flight.service';
import { Flights } from '../Interfaces';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html'
})
export class AboutComponent { 

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string,
    protected flightService: FlightService
  ) {

  }

}
