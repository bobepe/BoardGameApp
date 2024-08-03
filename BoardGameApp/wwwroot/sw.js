var cacheName = 'bg-pwa-v1.0.1';
var filesToCache = [
    './', // Domovská stránka
    './index.html',
    './Home/Privacy', // Endpoint Privacy
    './Home/Users', // Endpoint Users
    './css/style.css',
    './js/site.js'
];

/* Spusteni service workera a kesovani veskereho obsahu */
self.addEventListener('install', function (e) {
    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            return cache.addAll(filesToCache);
        })
    );
});

/* Aktivace nového service workera a vymazání staré cache */
self.addEventListener('activate', function (e) {
    e.waitUntil(
        caches.keys().then(function (keyList) {
            return Promise.all(keyList.map(function (key) {
                if (key !== cacheName) {
                    return caches.delete(key);
                }
            }));
        })
    );
    return self.clients.claim();
});

/* Zobrazeni kesovaneho obsahu v offline modu */
self.addEventListener('fetch', function (e) {
    e.respondWith(
        caches.match(e.request).then(function (response) {
            return response || fetch(e.request);
        })
    );
});