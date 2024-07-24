// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = () => {
    'use strict';

    if ('serviceWorker' in navigator) {
        navigator.serviceWorker
            .register('/sw.js')
            .then((registration) => {
                registration.onupdatefound = () => {
                    const installingWorker = registration.installing;
                    installingWorker.onstatechange = () => {
                        if (installingWorker.state === 'installed') {
                            if (navigator.serviceWorker.controller) {
                                // Nová aktualizace je k dispozici
                                console.log('Nový obsah je dostupný, prosím obnovte stránku.');
                                if (confirm("Nová verze aplikace je k dispozici. Chcete stránku nyní obnovit?")) {
                                    window.location.reload();
                                }
                            } else {
                                // Obsah je uložen pro offline použití
                                console.log('Obsah je uložen pro offline použití.');
                            }
                        }
                    };
                };
            })
            .catch((error) => {
                console.error('Chyba při registraci service workeru:', error);
            });
    }
};