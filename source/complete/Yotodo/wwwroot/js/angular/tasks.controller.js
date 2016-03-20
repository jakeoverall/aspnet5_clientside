(function() {
    'use strict';

    angular
        .module('app')
        .controller('TodoController', todoController);

    todoController.$inject = ["$http"];

    function todoController($http) {

        var vm = this;

        vm.items = [];
        vm.todo = {};
        
        vm.add = add;
        vm.complete = complete;
        
        activate();

        function activate() {
            loadTasks();
        }
        
        function loadTasks(){
            $http.get("/api/todos").then(
                function(response){
                    vm.items = response.data;
                }
            )
        }
        
        function add(){
            
           $http.post("/api/todos", vm.todo).then(
                function(response){
                    vm.items.push(response.data);
                }
           );
        }
        
        function complete(t){
            $http.put("/api/todos/complete/" + t.id).then(
                function(response){
                    vm.items.splice(vm.items.indexOf(t), 1);      
                }
            );
        }
    }
})();