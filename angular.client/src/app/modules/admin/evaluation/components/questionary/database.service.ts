import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class DatabaseService {
    private http = inject(HttpClient);
    private db: IDBDatabase;

    constructor() {
        this.openDatabase();
    }

    private openDatabase() {
        const request = indexedDB.open('PruebaPsicologica', 1);

        request.onupgradeneeded = (event) => {
            this.db = request.result;
            // Realiza las actualizaciones de esquema necesarias aquí
            if (!this.db.objectStoreNames.contains('questions')) {
                this.db.createObjectStore('questions', { keyPath: 'id' });
            }
            if (!this.db.objectStoreNames.contains('preguntasPsicologicas')) {
                this.db.createObjectStore('preguntasPsicologicas', { keyPath: 'id' });
            }
        };

        request.onsuccess = (event) => {
            this.db = request.result;
        };

        request.onerror = (event) => {
            console.error('Error al abrir la base de datos:', event);
        };
    }

    async ready(): Promise<void> {
        if (this.db) return;
        return new Promise<void>((resolve) => {
            const interval = setInterval(() => {
                if (this.db) {
                    clearInterval(interval);
                    resolve();
                }
            }, 100);
        });
    }

    private async getConnection(): Promise<IDBDatabase> {
        await this.ready();
        return this.db;
    }

    async getSavedQuestions(formId: number): Promise<any> {
        const db = await this.getConnection();
        const transaction = db.transaction(['questions'], 'readonly');
        const store = transaction.objectStore('questions');

        return new Promise((resolve, reject) => {
            const request = store.get(formId);
            request.onsuccess = function () {
                const data = request.result;
                if (data) {
                    // Devuelve las preguntas guardadas
                    resolve(data.questions);
                } else {
                    // Si no hay preguntas guardadas, devuelve un array vacío
                    resolve([]);
                }
            };
            request.onerror = function () {
                reject('Error al recuperar las preguntas guardadas');
            };
        });
    }

    async saveQuestions(questions: any[] = [], formId: number) {
        const db = await this.getConnection();
        db.transaction(['questions'], 'readwrite')
            .objectStore('questions')
            .put({ id: formId, questions });
    }

    async updateQuestion(formId: number, questionId: number, newAnswer: string) {
        const db = await this.getConnection();
        const transaction = db.transaction(['questions'], 'readwrite');
        const store = transaction.objectStore('questions');

        const request = store.get(formId);
        request.onsuccess = function () {
            let data = request.result;
            if (data) {
                // Encuentra la pregunta que necesita ser actualizada
                let question = data.questions?.find((q: any) => q.id === questionId);
                if (question) {
                    // Actualiza la respuesta de la pregunta
                    question.answer = newAnswer;
                } else {
                    // Si la pregunta no existe, crea una nueva
                    question = { id: questionId, answer: newAnswer };
                    data.questions.push(question);
                }
            } else {
                // Si el registro no existe, crea uno nuevo
                data = {
                    id: formId,
                    questions: [{ id: questionId, answer: newAnswer }]
                };
            }
            // Guarda el test psicológico actualizado de nuevo en la base de datos
            store.put(data);
        };
    }

    async deleteQuestions(formId: number) {
        const db = await this.getConnection();
        const transaction = db.transaction(['questions'], 'readwrite');
        const store = transaction.objectStore('questions');

        store.delete(formId);
    }


    async recuperarPreguntasPsicologicas(evaluacionPsicologicaId: number): Promise<any> {
        const db = await this.getConnection();
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(['preguntasPsicologicas'], 'readonly');
            const objectStore = transaction.objectStore('preguntasPsicologicas');
            const request = objectStore.get(evaluacionPsicologicaId);

            request.onsuccess = function (event) {
                if (request?.result?.preguntas) {
                    resolve(request.result.preguntas);
                } else {
                    resolve(null);
                }
            };

            request.onerror = function (event) {
                reject('Error al cargar las preguntas psicológicas');
            };
        });
    }



    async guardarPreguntasPsicologicas(data: any[]): Promise<void> {
        const db = await this.getConnection();
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(['preguntasPsicologicas'], 'readwrite');
            const objectStore = transaction.objectStore('preguntasPsicologicas');

            // Agrupar los datos por evaluacionPsicologicaId
            const groupedData = data.reduce((acc, item) => {
                const { evaluacionPsicologicaId } = item;
                if (!acc[evaluacionPsicologicaId]) {
                    acc[evaluacionPsicologicaId] = [];
                }
                acc[evaluacionPsicologicaId].push({
                    id: item.id,
                    text: item.text,
                    answer: item.answer,
                    order: item.order
                });
                return acc;
            }, {});

            // Guardar cada grupo en la base de datos bajo su evaluacionPsicologicaId
            const keys = Object.keys(groupedData);
            let pendingRequests = keys.length;

            keys.forEach(evaluacionPsicologicaId => {
                const preguntas = groupedData[evaluacionPsicologicaId];
                const request = objectStore.put({ id: Number(evaluacionPsicologicaId), preguntas });

                request.onsuccess = function (event) {
                    pendingRequests--;
                    if (pendingRequests === 0) {
                        resolve();
                    }
                };

                request.onerror = function (event) {
                    reject('Error al guardar las preguntas psicológicas');
                };
            });
        });
    }
}
