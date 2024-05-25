

    const maskOptions = {
        mask: '+{7}(000)000-00-00'
    };
    document.querySelectorAll('.phone').forEach(element => {
        IMask(element, maskOptions);
    });


    document.addEventListener("DOMContentLoaded", function () {
        // Получаем элементы
        var modal = document.getElementById("modal");
    var btn = document.getElementById("openModal");
    var closeBtn = document.getElementsByClassName("close")[0];
    var form = document.getElementById("contactForm");
    var thankYouModal = document.getElementById("thankYouModal");
    var area = document.getElementById("area");
    var name = document.getElementById("name");
    var phone = document.getElementById("phone");
    var thankYouCloseBtn = document.getElementsByClassName("thank-you-close")[0];


    // Открываем модальное окно при клике на кнопку
    btn.onclick = function () {
        modal.style.display = "block";
    name.value = ""; // Очищаем поле имени
    phone.value = ""; // Очищаем поле телефона
    area.value = ""; 

        }

    // Закрываем модальное окно при клике на "закрыть"
    closeBtn.onclick = function () {
        modal.style.display = "none";
        }

    // Закрываем модальное окно, если пользователь кликнул вне его
    window.onclick = function (event) {
            if (event.target == modal) {
        modal.style.display = "none";
            }
        }

    // Валидация и обработка формы
    form.onsubmit = function (event) {
        event.preventDefault(); // Предотвращаем отправку формы

    // Проверяем введенные данные
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;

    if (name.trim() === "" || phone.trim() === "") {
        alert("Пожалуйста, заполните все поля");
    return false;
            } else {
        // Здесь вы можете добавить код для отправки данных на сервер или выполнения других действий
        // После успешной отправки формы, закрываем текущее модальное окно и открываем модальное окно благодарности
        modal.style.display = "none";
    thankYouModal.style.display = "block";
    return true;
            }
        }

    // Закрываем модальное окно благодарности при клике на "закрыть"
    thankYouCloseBtn.onclick = function () {
        thankYouModal.style.display = "none";
        }

    // Закрываем модальное окно благодарности, если пользователь кликнул вне его
    window.onclick = function (event) {
            if (event.target == thankYouModal) {
        thankYouModal.style.display = "none";
            }
        }
    });


