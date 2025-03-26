const form = document.getElementById('classForm');
const table = document.getElementById('classTable').querySelector('tbody');
const classList = [];

form.addEventListener('submit', function (e) {
  e.preventDefault();

  const name = document.getElementById('className').value;
  const people = document.getElementById('peopleCount').value;
  const desc = document.getElementById('description').value;

  const entry = { name, people, desc };
  classList.push(entry);

  const row = document.createElement('tr');
  row.innerHTML = `<td>${name}</td><td>${people}</td><td>${desc}</td>`;
  table.appendChild(row);

  form.reset();
});

// Click on row
table.addEventListener('click', (e) => {
  if (e.target.tagName === 'TD') {
    const row = e.target.parentElement;
    console.log('Clicked row:', row.innerText);
    row.style.background = '#ffe600';
    setTimeout(() => row.style.background = '', 500);
  }
});

// Mouseover + Mouseout highlight
table.addEventListener('mouseover', e => {
  if (e.target.tagName === 'TD') {
    e.target.parentElement.style.backgroundColor = '#222';
  }
});
table.addEventListener('mouseout', e => {
  if (e.target.tagName === 'TD') {
    e.target.parentElement.style.backgroundColor = '';
  }
});

// Double-click to remove row
table.addEventListener('dblclick', e => {
  if (e.target.tagName === 'TD') {
    const row = e.target.parentElement;
    row.remove();
  }
});

// Input focus + blur styling
document.querySelectorAll('input, textarea').forEach(input => {
  input.addEventListener('focus', () => {
    input.style.borderColor = '#00f6ff';
    input.style.boxShadow = '0 0 8px #00f6ff';
  });

  input.addEventListener('blur', () => {
    input.style.borderColor = '';
    input.style.boxShadow = '';
  });
});
