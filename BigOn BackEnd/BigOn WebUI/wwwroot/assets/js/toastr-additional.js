/*toastr["success"]("message", "title")*/

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

window.addEventListener('load', function(){
    [...document.querySelectorAll('.pcoded-navbar ul:not(:has(li))')].forEach(item => {
        let parent = item.closest('li.pcoded-hasmenu');
        if (parent == null) return;
        parent.parentElement.removeChild(parent);
    })


})