﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Model;
using BasicAPP.Utils;
using BasicAPP.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BasicAPP.ViewModel
{
    public partial class DatosGridViewModel : ViewModelBase
    {
        private readonly IVolantesApiProvider _volantesService;
        private MainViewModel _mainViewModel;

        private int _currentPage = 1;
        private readonly int _itemsPerPage = 2;

        public DatosGridViewModel(IVolantesApiProvider volantesService, MainViewModel mainViewModel)
        {
            _volantesService = volantesService;
            _mainViewModel = mainViewModel;
            DatosGridItem = new ObservableCollection<ItemGridModel>();
            PaginatedItems = new ObservableCollection<ItemGridModel>();
            LoadVolantesAsync();
        }

        [ObservableProperty]
        private ObservableCollection<ItemGridModel> _DatosGridItem;

        [ObservableProperty]
        private ObservableCollection<ItemGridModel> _PaginatedItems;

        
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
                RefreshPaginatedItems();
            }
        }

        public int PageCount => (DatosGridItem.Count + _itemsPerPage - 1) / _itemsPerPage;

        private void RefreshPaginatedItems()
        {
            PaginatedItems.Clear();
            var items = DatosGridItem
                .Skip((CurrentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            foreach (var item in items)
            {
                PaginatedItems.Add(item);
            }
        }

        [RelayCommand]
        public void MoveToNextPage()
        {
            if (CurrentPage < PageCount)
            {
                CurrentPage++;
            }
        }

        [RelayCommand]
        public void MoveToPreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        private async Task LoadVolantesAsync()
        {
            DatosGridItem.Clear();
            try
            {
                var volantes = await _volantesService.GetAsync();
                if (volantes != null)
                {
                    foreach (var volante in volantes)
                    {
                        DatosGridItem.Add(ItemGridModel.CreateModelFromDTO(volante));
                    }
                }
                RefreshPaginatedItems();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private async Task AddItem()
        {
            var viewModel = new AddItemViewModel(_volantesService);
            var view = new AddItemView { DataContext = viewModel };
            view.ShowDialog();
            await LoadVolantesAsync();
        }

        [RelayCommand]
        public async Task Logout()
        {
            _mainViewModel.SelectedViewModel = _mainViewModel.LoginViewModel;
        }

        public override async Task LoadAsync()
        {
            await LoadVolantesAsync();
        }
    }
}
