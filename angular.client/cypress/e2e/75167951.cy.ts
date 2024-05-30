userEmail = '75167951';

describe('Simulación de inicio de sesión', () => {
    let registro = { user: userEmail, estadoApi: true, userId: null, succeeded: null, link: null };

    it(`Inicia sesión como ${userEmail}`, () => {
        // Intercepta las solicitudes específicas y asigna un alias
        cy.intercept('POST', 'http://26.97.164.113/auth/login').as('login');
        cy.intercept('POST', 'http://26.97.164.113/role/access/validate').as('validateAccess');
        cy.intercept('GET', 'http://26.97.164.113/auth/navigation').as('navigation');

        cy.visit('/sign-in');

        // Ingresa los datos del usuario
        cy.get('input[id=email]', { timeout: 20000 }).should('be.visible').type(userEmail);


        cy.get('input[id=password]').should('be.visible').type(userEmail);


        cy.get('button[type=submit]').should('be.visible').click();


        // Espera a que se complete cada llamada a la API y registra la respuesta
        cy.wait('@login', { timeout: 20000 }).then(({ request, response }) => {
            // Verifica que response, body, user e id existen
            if (response && response.body && response.body.user && response.body.user.id) {
                // Guarda el id del usuario
                registro.userId = response.body.user.id;
            } else {
                registro.estadoApi = false;
            }
        });

        cy.wait('@validateAccess', { timeout: 20000 }).then(({ request, response }) => {
            // Verifica que succeeded es true
            if (response.body.succeeded !== true) {
                registro.estadoApi = false;
            }
            registro.succeeded = response.body.succeeded;
        });

        cy.wait('@navigation', { timeout: 20000 }).then(({ request, response }) => {
            // Obtiene el primer enlace de compact
            if (response.body.compact.length > 0) {
                registro.link = response.body.compact[0].link;
            } else {
                registro.estadoApi = false;
            }
        });
    });


    after(() => {
        assert.isTrue(registro.estadoApi, `El estado de la API no es válido: ${JSON.stringify(registro)}`);
    });
});
