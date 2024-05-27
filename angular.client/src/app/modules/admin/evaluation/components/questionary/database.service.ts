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
    async getSavedQuestions(formId: number): Promise<any> {
    const db = await this.connection();
    const transaction = db.transaction(['questions'], 'readonly');
    const store = transaction.objectStore('questions');

    return new Promise((resolve, reject) => {
        const request = store.get(formId);
        request.onsuccess = function() {
            const data = request.result;
            if (data) {
                // Devuelve las preguntas guardadas
                resolve(data.questions);
            } else {
                // Si no hay preguntas guardadas, devuelve un array vacío
                resolve([]);
            }
        };
        request.onerror = function() {
            reject('Error al recuperar las preguntas guardadas');
        };
    });
}

    async saveQuestions(questions: any[] = [], formId: number) {
        const db = await this.connection();
        console.log('formId', formId);
        db.transaction(['questions'], 'readwrite')
            .objectStore('questions')
            .put({ id: formId, questions });
    }
    async updateQuestion(formId: number, questionId: number, newAnswer: string) {
        const db = await this.connection();
        const transaction = db.transaction(['questions'], 'readwrite');
        const store = transaction.objectStore('questions');

        const request = store.get(formId);
        request.onsuccess = function() {
            const data = request.result;
            if (data) {
                // Encuentra la pregunta que necesita ser actualizada
                const question = data.questions.find((q: any) => q.id === questionId);
                if (question) {
                    // Actualiza la respuesta de la pregunta
                    question.answer = newAnswer;
                }
                // Guarda el test psicológico actualizado de nuevo en la base de datos
                store.put(data);
            }
        };
    }

    async deleteQuestions(formId: number) {
        const db = await this.connection();
        const transaction = db.transaction(['questions'], 'readwrite');
        const store = transaction.objectStore('questions');

        store.delete(formId);
    }


}
