



window.addEventListener('load', function(event){
    time = new Date();
    updateTimestring();
    setInterval(
        function () {
            time = new Date();
            updateTimestring();
        }
    , 1000);
})

function updateTimestring() {
    document.getElementById("timer").innerText = time.toLocaleString();
}

function validateForm(){
    alert("Send Successfully");
    return true;
}