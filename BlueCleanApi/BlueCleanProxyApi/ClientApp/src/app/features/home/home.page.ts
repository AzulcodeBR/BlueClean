import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-home-page',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './home.page.html',
  styleUrl: './home.page.css'
})
export class HomePage {}
