// Write your Javascript code.
!function () {
  let elemWell = $('body').append("<div></div>");
  elemWell.addClass('well');
  elemWell.append("<button class='btn btn-primary' onclick='my.sayHello()'>Say Hello</button>");

  function sayHello() {
    alert('Hello at ' + new Date().toISOString());
  }

  window.my = {
    sayHello: sayHello
  }
}();