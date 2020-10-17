import { Component, OnInit } from '@angular/core';
import { ImplicitReceiver } from '@angular/compiler';

declare const ShowCarousel: any;
declare const ShowSlider: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  ShowSliderHome() {
    ShowSlider();
  }
  ShowCarouselHome() {
    ShowCarousel();
  }
}
