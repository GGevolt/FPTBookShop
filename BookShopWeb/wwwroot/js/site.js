const menuOptions = document.querySelectorAll('.nav-link');

// Event listeners for mouseenter and mouseleave to each menu option
menuOptions.forEach(menuOption => {
    menuOption.addEventListener('mouseenter', expandItem);
    menuOption.addEventListener('mouseleave', shrinkItem);
});

// Function to expand the item
function expandItem() {
    // Increase the size of the item
    this.style.transform = 'scale(1.4)';
    // Change the background color to red
    this.style.backgroundColor = 'red';
    // Change the text color to white
    this.style.color = 'white';
    // Position the expanded item relatively
    this.style.position = 'relative';
    // Set a higher z-index value to make the item appear behind other elements
    this.style.zIndex = '1';
}

// Function to shrink the item
function shrinkItem() {
    // Reset the size of the item
    this.style.transform = 'scale(1)';
    // Change the background color back to white
    this.style.backgroundColor = 'white';
    // Change the text color back to black
    this.style.color = 'black';
    // Reset the position and z-index values
    this.style.position = 'static';
    this.style.zIndex = 'initial';
}