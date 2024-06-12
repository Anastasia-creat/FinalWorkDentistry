 const maskOptions = {
        mask: '+{7}(000)000-00-00'
    };
    document.querySelectorAll('.phone').forEach(element => {
        IMask(element, maskOptions);
    });
    document.addEventListener("DOMContentLoaded", function () {
        var modal = document.getElementById("modal");
    var btn = document.getElementById("openModal");
    var closeBtn = document.getElementsByClassName("close")[0];
    var form = document.getElementById("contactForm");
    var thankYouModal = document.getElementById("thankYouModal");
    var area = document.getElementById("area");
    var name = document.getElementById("name");
    var phone = document.getElementById("phone");
    var thankYouCloseBtn = document.getElementsByClassName("thank-you-close")[0];
    btn.onclick = function () {
        modal.style.display = "block";
    name.value = ""; // Очищаем поле имени
    phone.value = ""; // Очищаем поле телефона
    area.value = ""; 
        }
    closeBtn.onclick = function () {
        modal.style.display = "none";
        }
    window.onclick = function (event) {
            if (event.target == modal) {
        modal.style.display = "none";
            }
       }
    form.onsubmit = function (event) {
        event.preventDefault(); 
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;
    if (name.trim() === "" || phone.trim() === "") {
        alert("Пожалуйста, заполните все поля");
    return false;
            } else {
        modal.style.display = "none";
    thankYouModal.style.display = "block";
    return true;
            }
        }
    thankYouCloseBtn.onclick = function () {
        thankYouModal.style.display = "none";
        }
    window.onclick = function (event) {
            if (event.target == thankYouModal) {
        thankYouModal.style.display = "none";
            }
        }
    });