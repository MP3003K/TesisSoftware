import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private http = inject(HttpClient);

    connection(): Promise<IDBDatabase> {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open('questionary', 3);
            let db: IDBDatabase;

            request.onerror = function (event) {
                reject('Error al abrir la base de datos');
            };

            request.onsuccess = function (event) {
                db = request.result;
                console.log('Base de datos abierta correctamente');
                resolve(db);
            };

            request.onupgradeneeded = function (event) {
                const target = event.target as IDBOpenDBRequest;
                db = target.result;
                db.createObjectStore('questions', {
                    keyPath: 'id',
                });
                resolve(db);
            };
        });
    }
    start() {
        this.connection();
    }
    async saveQuestions(questions: any[] = [], formId: number) {
        const db = await this.connection();
        console.log(questions)
        db.transaction(['questions'], 'readwrite')
            .objectStore('questions')
            .put({ id: formId, questions });
    }
}
