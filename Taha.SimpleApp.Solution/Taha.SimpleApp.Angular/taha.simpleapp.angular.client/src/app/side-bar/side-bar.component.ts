import { Component } from '@angular/core';
import {CategoryService} from './category.service';
import {category} from '../types/entities';
import {RouterLink, RouterLinkActive} from '@angular/router';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
  categories: category[] = [];
  showPopup: boolean = false;

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  openAddCategoryPopup(): void {
    this.showPopup = true;
  }

  closePopup(): void {
    this.showPopup = false;
  }

  addCategory(categoryName: string): void {
    // Logic to add a category, e.g., call a service method
    this.categoryService.addCategory(categoryName).subscribe(() => {
      this.fetchCategories();
      this.closePopup();
    });
  }
}
