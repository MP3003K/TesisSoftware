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
                    resolve(data);
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

    async updateQuestion(formId: number, esEmocional: boolean, questionId: number, newAnswer: string) {
    try {
        const db = await this.connection();
        const transaction = db.transaction(['respuestasTesis'], 'readwrite');
        const store = transaction.objectStore('respuestasTesis');

        const request = store.get(formId);
        const data = await new Promise<any>((resolve, reject) => {
            request.onsuccess = () => resolve(request.result);
            request.onerror = () => reject(new Error('Error al obtener el formulario'));
        });

        if (data) {
            const question = data.preguntas.find((q: any) => q.id === questionId && q.esEmocional === esEmocional);
            if (question) {
                question.answer = newAnswer;
                question.esEmocional = esEmocional;
            } else {
                data.preguntas.push({ id: questionId, esEmocional: esEmocional, answer: newAnswer });
            }
            await new Promise<void>((resolve, reject) => {
                const updateRequest = store.put(data);
                updateRequest.onsuccess = () => resolve();
                updateRequest.onerror = () => reject(new Error('Error al actualizar la pregunta'));
            });
        } else {
            // Si no existe el formulario, crearlo con la nueva pregunta
            const newData = { id: formId, preguntas: [{ id: questionId, esEmocional: esEmocional,answer: newAnswer }] };
            await new Promise<void>((resolve, reject) => {
                const addRequest = store.put(newData);
                addRequest.onsuccess = () => resolve();
                addRequest.onerror = () => reject(new Error('Error al crear el formulario'));
            });
        }
    } catch (error) {
        console.error(error.message);
    }
}

    async eliminarRespuestasGuardadas(formId: number, esEmocional: boolean) {
        try {
            const db = await this.connection();
            const transaction = db.transaction(['respuestasTesis'], 'readwrite');
            const store = transaction.objectStore('respuestasTesis');

            const request = store.get(formId);
            const data = await new Promise<any>((resolve, reject) => {
                request.onsuccess = () => resolve(request.result);
                request.onerror = () => reject(new Error('Error al obtener el formulario'));
            });

            if (data) {
                // Filtrar las preguntas para eliminar aquellas que coincidan con esEmocional
                data.preguntas = data.preguntas.filter((q: any) => q.esEmocional !== esEmocional);

                if (data.preguntas && data.preguntas.length === 0) {
                    // Si no hay preguntas restantes, eliminar el registro completo
                    await new Promise<void>((resolve, reject) => {
                        const deleteRequest = store.delete(formId);
                        deleteRequest.onsuccess = () => resolve();
                        deleteRequest.onerror = () => reject(new Error('Error al eliminar el formulario'));
                    });
                } else {
                    // Si hay preguntas restantes, actualizar el formulario
                    await new Promise<void>((resolve, reject) => {
                        const updateRequest = store.put(data);
                        updateRequest.onsuccess = () => resolve();
                        updateRequest.onerror = () => reject(new Error('Error al actualizar el formulario'));
                    });
                }
            } else {
                console.log('No se encontraron respuestas guardadas.');
            }
        } catch (error) {
            console.error(error.message);
        }
    }

}
