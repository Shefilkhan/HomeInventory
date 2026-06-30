// site.js - Premium client-side functionalities for Home Inventory System

$(function () {
    // 1. Initialize Flatpickr Datepicker with jQuery Validation Integration
    if (typeof flatpickr !== 'undefined') {
        flatpickr(".datepicker", {
            dateFormat: "Y-m-d",
            allowInput: true,
            disableMobile: "true", // Force custom picker on mobile for styling consistency
            onChange: function (selectedDates, dateStr, instance) {
                // Manually trigger standard change and blur events so jQuery Unobtrusive Validation is run
                $(instance.element).trigger("change").trigger("blur");
            }
        });
    }

    // 2. Interactive Password Show/Hide Toggle Handler
    $('.password-toggle-btn').on('click', function (e) {
        e.preventDefault();
        const btn = $(this);
        const input = btn.closest('.input-group').find('input');
        const icon = btn.find('i');

        if (input.attr('type') === 'password') {
            input.attr('type', 'text');
            icon.removeClass('bi-eye').addClass('bi-eye-slash');
            btn.attr('aria-label', 'Hide password');
        } else {
            input.attr('type', 'password');
            icon.removeClass('bi-eye-slash').addClass('bi-eye');
            btn.attr('aria-label', 'Show password');
        }
    });

    // 3. SweetAlert2 confirmation modal for actions
    $(document).on('click', '.btn-delete-confirm', function (e) {
        e.preventDefault();
        const button = $(this);
        const form = button.closest('form');
        const itemName = button.data('item-name') || 'this item';

        if (typeof Swal !== 'undefined') {
            Swal.fire({
                title: 'Delete Item?',
                text: `Are you sure you want to permanently delete "${itemName}"? This action cannot be undone.`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#ef4444', // Match --color-danger
                cancelButtonColor: 'rgba(255, 255, 255, 0.1)',
                confirmButtonText: 'Yes, Delete It',
                cancelButtonText: 'Cancel',
                background: '#0f1524',
                color: '#f8fafc',
                customClass: {
                    popup: 'swal-glass-popup',
                    confirmButton: 'btn btn-danger-gradient px-4',
                    cancelButton: 'btn btn-outline-glass px-4 ms-2'
                },
                buttonsStyling: false
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
        } else {
            // Fallback to native browser confirmation if SweetAlert2 failed to load
            if (confirm(`Are you sure you want to delete "${itemName}"?`)) {
                form.submit();
            }
        }
    });
});
