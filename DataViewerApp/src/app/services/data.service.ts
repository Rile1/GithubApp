import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GitRepository } from '../models/git-repository.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'http://localhost:5218/account';

  constructor(private httpClient: HttpClient) { }

  login() {
    window.location.href = this.apiUrl + '/login-with-github';
  }

  getData(): Observable<GitRepository[]> {
    return this.httpClient.get<GitRepository[]>(this.apiUrl + "/repositories", {
      withCredentials: true
    });
  }

  saveRepositories(repositories: GitRepository[]): Observable<boolean> {
    return this.httpClient.post<boolean>(this.apiUrl + "/save", repositories, {
      withCredentials: true
    })
  }
}
