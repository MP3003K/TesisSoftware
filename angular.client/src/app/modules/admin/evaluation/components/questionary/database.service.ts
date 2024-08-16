import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private http = inject(HttpClient);

    connection(): Promise<IDBDatabase> {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open('respuestasTesis', 3);
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
                db.createObjectStore('respuestasTesis', {
                    keyPath: 'id',
                });
                resolve(db);
            };
        });
    }

    start() {
        this.connection();
    }

    async getSavedQuestions(formId: number): Promise<any> {
        const db = await this.connection();
        const transaction = db.transaction(['respuestasTesis'], 'readonly');
        const store = transaction.objectStore('respuestasTesis');

        return new Promise((resolve, reject) => {
            const request = store.get(formId);
            request.onsuccess = function() {
                const data = request.result;
                if (data) {
                    resolve(data.preguntas);
                } else {
                    resolve([]);
                }
            };
            request.onerror = function() {
                reject('Error al recuperar las preguntas guardadas');
            };
        });
    }



    async saveQuestions(preguntas: any[] = [], formId: number) {
        const db = await this.connection();
        db.transaction(['respuestasTesis'], 'readwrite')
            .objectStore('respuestasTesis')
            .put({ id: formId, preguntas });
    }

async updateQuestion(formId: number, questionId: number, newAnswer: string) {
    const db = await this.connection();
    const transaction = db.transaction(['respuestasTesis'], 'readwrite');
    const store = transaction.objectStore('respuestasTesis');

    const request = store.get(formId);
    request.onsuccess = function() {
        const data = request.result;
        if (data) {
            const question = data.preguntas.find((q: any) => q.id === questionId);
            if (question) {
                question.answer = newAnswer;
            } else {
                data.preguntas.push({ id: questionId, answer: newAnswer });
            }
            store.put(data);
        } else {
            // Si no existe el formulario, crearlo con la nueva pregunta
            const newData = { id: formId, preguntas: [{ id: questionId, answer: newAnswer }] };
            store.put(newData);
        }
    };
    request.onerror = function() {
        console.error('Error al actualizar la pregunta');
    };
}

    async deleteQuestions(formId: number) {
        const db = await this.connection();
        const transaction = db.transaction(['respuestasTesis'], 'readwrite');
        const store = transaction.objectStore('respuestasTesis');

        store.delete(formId);
    }

}
