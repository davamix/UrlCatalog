import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';

@Component({
    selector: 'catalog',
    template: require('./catalog.component.html')
})

export class CatalogComponent {
    public urls: BlogPost[];
    public blogPost: BlogPost;
    public http: Http;

    constructor(http: Http) {
        this.http = http;
        this.http.get('/api/CatalogData/BlogPosts').subscribe(result => {
            this.urls = result.json();
        });
        this.blogPost = {
            id: 1,
            title: "Title A",
            publishedDate:"10/10/2016",
            url: "http://some.post",
            comments: "bla bla bla",
            readDate: "12/10/2016"
        };
        
    }

    public saveUrl() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        this.http.post('/api/CatalogData/BlogPosts', this.blogPost, options);
    }

    public clearUrl() {
        this.blogPost = {
            id:-1,
            title: "",
            publishedDate: "",
            url: "",
            comments: "",
            readDate: ""
        };
    }
}

interface BlogPost {
    id: number;
    title: string;
    publishedDate: string;
    url: string;
    comments: string;
    readDate: string;
}