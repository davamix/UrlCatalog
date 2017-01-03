import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'catalog',
    template: require('./catalog.component.html')
})

export class CatalogComponent {
    public urls: BlogPost[];

    constructor(http: Http) {
        http.get('/api/CatalogData/BlogPosts').subscribe(result => {
            this.urls = result.json();
        });
    }
}

interface BlogPost {
    title: string;
    publishedDate: string;
}