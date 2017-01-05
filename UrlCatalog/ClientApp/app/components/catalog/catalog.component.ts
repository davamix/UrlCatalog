import { Component } from "@angular/core";
import { Http } from "@angular/http";
import { Headers, RequestOptions } from "@angular/http";

@Component({
    selector: "catalog",
    template: require("./catalog.component.html")
})

export class CatalogComponent {
    public urls: BlogPost[];
    public blogPost: BlogPost;
    public httpService: Http;
    public status: string;

    constructor(http: Http) {
        this.httpService = http;

        this.httpService.get("/api/Catalog").subscribe(result => {
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
        this.status = "Saving " + this.blogPost.title;

        let headers = new Headers({ 'Content-Type': 'application/json' });

        // Second call, after clearUrl, this is sending a null value to the Controller
        this.httpService.post("/api/Catalog", this.blogPost, { headers: headers })
            .subscribe(result => {
                this.status = result.json().data;
            });

        this.status = "Saved " + this.blogPost.title;
    }

    public clearUrl() {
        this.status = "Clearing...";

        this.blogPost = {
            id:-1,
            title: "",
            publishedDate: "",
            url: "",
            comments: "",
            readDate: ""
        };

        this.status = "Clear";
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