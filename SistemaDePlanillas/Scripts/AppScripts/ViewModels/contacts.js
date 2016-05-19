define(['jquery', 'knockout'], function ($, ko) {


    function ViewModel() {
        var self = this;

        self.contacts = [
            { name: 'Francisco Gómez', email: 'fgomeza25@gmail.com' },
            { name: 'Jonathan Charpentier', email: 'jonnch15j@outlook.com' },
            { name: 'Jafet Román', email: 'jafet21@hotmail.es' },
            { name: 'Oscar Chaves', email: 'andresch1009@gmail.com' },
            { name: 'Carlos Agüero', email: 'dev.aguero@gmail.com' },
        ];
    }

    return new ViewModel;
});