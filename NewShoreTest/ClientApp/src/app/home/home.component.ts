import { Component, OnInit, Inject, Input } from '@angular/core';
import { ImplicitReceiver } from '@angular/compiler';
import { HttpClient } from '@angular/common/http';
import { FlightService } from '../service/flight.service';
import { Flights } from '../Interfaces';
import { Observable } from 'rxjs';

declare const ShowCarousel: any;
declare const ShowSlider: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  
  public lstFlight: Observable<Flights[]>;
  bandera = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string,
    protected flightService: FlightService
  ) {
    this.GetFlights();
  }

  ShowSliderHome() {
    ShowSlider();
  }

  ShowCarouselHome() {
    ShowCarousel();
  }

  public ShowTable(estado) {
    if (estado) {
      this.bandera = estado;
    }
    else {
      this.bandera = estado;
    }
      
  }

  public GetFlights() {
    this.lstFlight = this.flightService.GetFlight();
  }
}


