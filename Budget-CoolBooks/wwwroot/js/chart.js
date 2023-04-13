const ctx = document.getElementById('myChart');

new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ["Art", "Biography", "Business", "Children's", "Christian"],
        datasets: [{
            label: 'Books',
            data: [4, 20, 18, 8, 5],
            borderWidth: 1,
            borderColor: '#36A2EB',
            backgroundColor: '#0d6efd',
        }]
    },
});