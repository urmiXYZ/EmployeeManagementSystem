document.addEventListener('DOMContentLoaded', () => {
    const nameInput = document.getElementById('NameInput');
    const nameError = document.getElementById('NameError');
    const submitBtn = document.querySelector('button[type="submit"]');

    if (!nameInput || !nameError || !submitBtn) return;

    if (nameInput.hasAttribute('maxlength')) {
        nameInput.removeAttribute('maxlength');
    }

    nameInput.addEventListener('input', () => {
        if (nameInput.value.length > 20) {
            nameError.textContent = 'Name cannot be longer than 20 characters.';
            submitBtn.disabled = true;
        } else {
            nameError.textContent = '';
            submitBtn.disabled = false;
        }
    });
});