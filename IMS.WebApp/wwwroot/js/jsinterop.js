function preventFormSubmitOnEnter(formId) {
    // console.log('Entered preventFormSubmitOnEnter for formId:', formId);
    const form = document.getElementById(formId);
    if (form) {
        form.addEventListener('keydown', function (event) {
            // console.log('Key pressed:', event.key);
            if (event.key === 'Enter') {
                event.preventDefault();
                // console.log('Form submission prevented on Enter key press.');
                return false;
            }
        });
    }
}