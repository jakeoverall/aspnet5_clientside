
$( document ).ready(function() {
    
    $(".todo-list-item input").on("click", function(){
        
        var el = $(this);
        var id = el.attr("id");
        
        $.ajax({
            url: '/todos/complete/' + id,
            type: 'PUT',
            success: function(result) {
                el.parent().remove();
            }
        });
    })
});