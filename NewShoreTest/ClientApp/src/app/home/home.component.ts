import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FlightService } from '../service/flight.service';
import { Flights } from '../Interfaces';
import DateTimeFormat = Intl.DateTimeFormat;

declare const ShowCarousel: any;
declare const ShowSlider: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public origin: string;
  public destination: string;
  public flightDate: string;
  public minDate: Date;

  public lstFlight: Flights[] = [];
  bandera = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string,
    protected flightService: FlightService
  ) {
    this.minDate = new Date();
  }

  ngOnInit(): void{
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

  public formValid() {
    return this.flightDate && this.destination && this.origin;
  }

  public LimpiarBusqueda(){
    this.lstFlight = [];
    this.ShowTable(false);
    this.origin = '';
    this.destination = '';
    this.flightDate = null;
  }

  public GetFlights() {
    this.lstFlight = [];
    this.ShowTable(false);
    this.flightService.GetFlight(this.origin, this.destination, this.flightDate).subscribe(data => {
      console.log(data);
      this.lstFlight = data;
      this.ShowTable(true);
    });
  }

  public SaveFlight(flight: Flights) {
    this.flightService.SaveFlight(flight).subscribe(data => {
      alert("El vuelo se guardó exitosamente")
      this.lstFlight = [];
      this.ShowTable(false);
    })
  }
}




