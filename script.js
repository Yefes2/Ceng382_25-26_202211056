// Store users here
const users = [];

document.querySelector('.login-form').addEventListener('submit', function (e) {
  e.preventDefault();

  const username = document.querySelector('input[type="text"]').value;
  const password = document.querySelector('input[type="password"]').value;

  users.push({ username, password });

  console.clear();
  console.log("Logged Users:");
  users.forEach((user, index) => {
    console.log(`${index + 1}. Username: ${user.username}, Password: ${user.password}`);
  });

  // Optionally clear fields
  this.reset();
});

function updateClock() {
    const clock = document.getElementById('liveClock');
    const now = new Date();
    clock.textContent = now.toLocaleTimeString();
  }
  
  setInterval(updateClock, 1000);
  updateClock(); // initial run

  let formsVisible = true;

document.addEventListener('keydown', function (e) {
  if (e.key.toLowerCase() === 'h') {
    formsVisible = !formsVisible;
    document.querySelectorAll('form').forEach(form => {
      form.style.display = formsVisible ? 'block' : 'none';
    });
  }
});

document.querySelector('.login-form').addEventListener('submit', function (e) {
  e.preventDefault();

  const username = document.querySelector('input[type="text"]').value;
  const password = document.querySelector('input[type="password"]').value;

  if (username === 'admin' && password === 'admin') {
    window.location.href = 'table.html';
  } else {
    alert('Incorrect username or password!');
  }

  this.reset();
});