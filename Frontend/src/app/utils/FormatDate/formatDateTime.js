export function formatDateTime(date) {
    if (!date) {
        return 'Invalid date';
    }

    if (typeof date === 'string') {
        date = new Date(date);
    }

    if (isNaN(date.getTime())) {
        return 'Invalid date';
    }

    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${hours}:${minutes} - ${year}/${month}/${day}`;
}