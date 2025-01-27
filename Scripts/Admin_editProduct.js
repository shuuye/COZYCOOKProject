document.addEventListener("DOMContentLoaded", function() {
    const editButtons = document.querySelectorAll(".edit");
    const addButtons = document.querySelectorAll(".add");
    const modal = document.getElementById('addProductForm');
    const tableBody = document.querySelectorAll('table tbody'); // Select all table bodies
    
    function addEditEventListeners() {
        const editButtons = document.querySelectorAll(".edit");
        editButtons.forEach(button => {
            button.addEventListener("click", function() {
                const row = this.closest("tr");
                const cells = row.querySelectorAll(".editable");
                
                // Toggle contenteditable attribute for all editable cells
                cells.forEach(cell => {
                    cell.contentEditable = !cell.isContentEditable;
                });
                
                // Change button text based on edit state
                const editButtonText = this.textContent;
                this.textContent = editButtonText === "Edit" ? "Save" : "Edit";
            });
        });
    }
    // Close the add product form when the close button is clicked
    const closeButton = document.querySelector('.close');
    closeButton.addEventListener('click', function() {
        const modal = document.getElementById('addProductForm');
        modal.style.display = "none";
    });
    // Show the add new product form when the "+" symbol is clicked
    const addNewProductButton = document.getElementById('addNewProductButton');
    addNewProductButton.addEventListener('click', function() {
        const modal = document.getElementById('addProductForm');
        modal.style.display = "block";
    });
 // -------------------------------------------------------Add event listeners to all add buttons-----------------------------------------------------
 const add = document.querySelectorAll(".add");
 addButtons.forEach(addButton => {
     addButton.addEventListener("click", function() {
         const modal = document.getElementById('addProductForm');
         modal.style.display = "block";
         
         // Set the default value of the product category dropdown to the table's category
         const category = this.closest('table').id === 'food-table' ? 'dishes' : 'beverage';
         document.getElementById('productCategory').value = category;
     });
 });
    // ---------------------------------------------------------Add event listener for remove buttons-------------------------------------------------------
    function addRemoveEventListeners() {
        const removeButtons = document.querySelectorAll('.remove');
        document.querySelectorAll('.remove').forEach(removeButton => {
        removeButton.addEventListener('click', function() {
            const row = this.parentNode.parentNode;
            row.remove();
        });
    });}
     
 
    

    //--------------------------------------------------------------------------Handle form submission--------------------------------------------------------------------

const addProductForm = document.getElementById('addProduct');
addProductForm.addEventListener('submit', function(event) {
    event.preventDefault();
    // Retrieve form data
    const productName = document.getElementById('productName').value;
    const productPrice = document.getElementById('productPrice').value;
    const productImage = document.getElementById('productImage').files[0];
    const productDescription = document.getElementById('productDescription').value;
    const productCategory = document.getElementById('productCategory').value;

    // Create a new row in the table

    const tableId = productCategory === 'dishes' ? 'food-table' : 'drink-table';
    const tableBody = document.getElementById(tableId).querySelector('tbody');
    const newRow = document.createElement('tr');
    const newImageCell = document.createElement('td');
    const newImage = document.createElement('img');
    newImage.src = URL.createObjectURL(productImage); // Set source of the image
    newImage.alt = productName; // Set alt text for the image
    newImageCell.appendChild(newImage);
    newRow.appendChild(newImageCell);
    newRow.innerHTML += `
        <td><span class="editable">${productName}</span></td>
        <td><span class="editable">$${productPrice}</span></td>
        <td><span class="editable">${productDescription}</span></td>
        <td><button class="edit">Edit</button></td>
        <td><button class="remove">Remove</button></td>
  
    `;
    tableBody.appendChild(newRow);
    
    // Reset the form
    addProductForm.reset();
    
    // Close the modal
        addEditEventListeners();
        addRemoveEventListeners();
    });
    
    // Initial event listeners for existing edit and remove buttons
    addEditEventListeners();
    addRemoveEventListeners();


    // Add event listener for save button
    const saveButton = document.getElementById('save-btn');
    saveButton.addEventListener('click', function() {
        const rows = document.querySelectorAll('#food-table tbody tr');
        rows.forEach(row => {
            const cells = row.querySelectorAll('td');
            // Start from index 1 to exclude photo and actions
            for (let i = 1; i < cells.length - 1; i++) {
                const cell = cells[i];
                const input = cell.querySelector('input');
                if (input) {
                    const value = input.value;
                    cell.innerHTML = `<span class="editable">${value}</span>`;
                }
            }
        });
    });
    
});
