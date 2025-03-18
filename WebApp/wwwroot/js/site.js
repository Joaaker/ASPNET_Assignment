﻿


//Dark Mode
// Generated by ChatGPT-03-high - Syncs both Dark mode toggles (Hamburger and Settings)
// Also check if the user prefers dark mode and sets to dark mode 
document.addEventListener('DOMContentLoaded', function () {
    const defaultToggle = document.getElementById('dark-mode');
    const hamburgerToggle = document.getElementById('hamburger-dark-mode');

    function updateDarkMode(isDark) {
        if (isDark) {
            document.body.classList.remove('light');
            document.body.classList.add('dark');
        } else {
            document.body.classList.remove('dark');
            document.body.classList.add('light');
        }
        if (defaultToggle) defaultToggle.checked = isDark;
        if (hamburgerToggle) hamburgerToggle.checked = isDark;
    }

    const prefersDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    updateDarkMode(prefersDark);

    if (defaultToggle) {
        defaultToggle.addEventListener('change', function () {
            updateDarkMode(this.checked);
        });
    }
    if (hamburgerToggle) {
        hamburgerToggle.addEventListener('change', function () {
            updateDarkMode(this.checked);
        });
    }
});


// Generated by ChatGPT-03-high - Makes the Dark Mode div clickable without closing the drop-down settings ul
document.querySelector('.dark-mode-container').addEventListener('click', function (e) { e.stopPropagation(); });




//Hamburger menu
const hamburgerBtn = document.getElementById('hamburgerBtn');
const menu = document.getElementById('menu');

hamburgerBtn.addEventListener('click', function() {
    menu.classList.toggle('active');
});

//Modal
document.addEventListener('DOMContentLoaded', () => {

    // Open Buttons
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal) {
                modal.style.display = 'flex';
                document.body.classList.add('modal-open');
            }

        })
    })


    // Close Buttons
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-container')
            if (modal) {
                modal.style.display = 'none'
                document.body.classList.remove('modal-open');

                //Clear Formdata
            }
        })
    })
})