import { ActiveDescendantKeyManager } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Activities } from 'src/app/models/activities';
import { ActivitiesService } from 'src/app/services/activities.service';


const ACT_DATA: Activities[] = [];

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.scss']
})
export class ActivitiesComponent implements OnInit {

  dataSource = ACT_DATA;
  displayedColumns: string[] = ['fechaActividad', 'nombreCompleto', 'detalle'];

  @ViewChild(MatTable) table!: MatTable<Activities>;
  
  constructor(private ActivitiesService: ActivitiesService) { }

  ngOnInit(): void {
    this.ActivitiesService.getActivities().subscribe((result: any) => {
      result.forEach((e: any) => {
        let date = e.fechaCreacion;
        let act = new Activities(
          date.split('T')[0],
          e.nombreUsuario,
          e.detalle
        )
        ACT_DATA.push(act)
      });
      this.dataSource = ACT_DATA;
      this.table.renderRows();
    })
  }


}
