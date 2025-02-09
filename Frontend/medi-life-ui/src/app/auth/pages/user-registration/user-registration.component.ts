import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';


@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit {

  registrationForm!: FormGroup;
  submitted = false;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      fullName: ['', [Validators.required]], 
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      mobileNumber: [
        '', 
        [
          Validators.required, 
          Validators.pattern(/^\d{10}$/) // Ensures exactly 10 digits
        ]
      ],
      passwordHash: [
        '', 
        [
          Validators.required, 
          Validators.minLength(6), 
          Validators.maxLength(100)
        ]
      ]      
    });
  }

  get f() {
    return this.registrationForm.controls;
  }

  onSubmit() {
    this.submitted = true;
  
    if (this.registrationForm.invalid) {
      return; // Stop if the form is invalid
    }
  
    this.authService.register(this.registrationForm.value).subscribe({
      next: () => {        
        this.successMessage = "User registered successfully!";
        this.errorMessage = "";
        this.registrationForm.reset(); // Reset the form after successful registration
        this.submitted = false;
      },
      error: () => {
        this.errorMessage = "Registration failed. Try again.";
        this.successMessage = "";
      },
    });
    setTimeout(() => {
      this.successMessage = "";
      this.errorMessage = "";
    }, 5000);    
  }
  

}
