import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';
import { GitRepository } from '../../models/git-repository.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-repos',
  templateUrl: './repos.component.html',
  styleUrl: './repos.component.css'
})
export class ReposComponent implements OnInit {
  repositories: GitRepository[] = [];
  editingIndex: number = -1;
  newDescription: string = "";
  saving: boolean = false;
  
  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit() {
    this.dataService.getData().subscribe(
      (result: GitRepository[]) => { 
        this.repositories = result;
      },
      (error) => {
        this.router.navigateByUrl("/unauthorized");
      }
    );
  }

  editDescription(index: number) {
    this.newDescription = this.repositories[index].description ?? "";
    this.editingIndex = index;
  }

  cancelEditDescription() {
    this.editingIndex = -1;
  }

  saveDescription() {
    this.repositories[this.editingIndex].description = this.newDescription;
    this.editingIndex = -1;
  }

  storeRepositories() {
    this.saving = true;
    this.dataService.saveRepositories(this.repositories).subscribe(
      (result: boolean) => { 
        this.saving = false;
      }
    );
  }
}
